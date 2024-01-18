using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AutonomousAgent : AI_Agent {
    [SerializeField] AI_Perception seekPerception = null;
    [SerializeField] AI_Perception fleePerception = null;
    [SerializeField] AI_Perception flockPerception = null;
    [SerializeField] AI_Perception obstaclePerception = null;

    private void Update() {
        if (seekPerception != null) { 
            var gameObjects = seekPerception.getGameObjects();
            if (gameObjects.Length > 0) {
            movement.applyForce(seek(gameObjects[0]));
            }
        }
        if (fleePerception != null) {
            var gameObjects = fleePerception.getGameObjects();
            if (gameObjects.Length > 0) {
                movement.applyForce(flee(gameObjects[0]));
            }
        }
        if (flockPerception != null) {
            var gameObjects = flockPerception.getGameObjects();
            if (gameObjects.Length > 0) {
                movement.applyForce(cohesion(gameObjects));
                movement.applyForce(seperation(gameObjects, 3));
                movement.applyForce(alignment(gameObjects));
            }
        }
        // obstacle avoidance
        if (obstaclePerception != null) {
            if (((AI_SpherecastPerception)obstaclePerception).CheckDirection(Vector3.forward)) {
                Vector3 open = Vector3.zero;
                if (((AI_SpherecastPerception)obstaclePerception).GetOpenDirection(ref open)) {
                    movement.applyForce(getSteeringForce(open) * 5);
                }
			}
        }

        Vector3 acceleration = movement.Acceleration;
        acceleration.y = 0;
        movement.Acceleration = acceleration;

        transform.position = Utilities.wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
    }

    private Vector3 flee(GameObject target) {
        Vector3 direction =  transform.position - target.transform.position;
        return getSteeringForce(direction);
    }

    private Vector3 seek(GameObject target) {
        Vector3 direction = target.transform.position - transform.position;
        return getSteeringForce(direction);
    }

    private Vector3 cohesion(GameObject[] neighbors) {
        Vector3 positions = Vector3.zero;
        foreach (var neighbor in neighbors) {
            positions += neighbor.transform.position;
        }

        Vector3 center = positions / neighbors.Length;
        Vector3 direction = center - transform.position;

        return getSteeringForce(direction);
    }

    private Vector3 seperation(GameObject[] neighbors, float radius) {
        Vector3 seperation = Vector3.zero;
        foreach (var neighbor in neighbors) {
            Vector3 direction = (transform.position - neighbor.transform.position);
            if (direction.magnitude < radius) {
                seperation += direction / direction.sqrMagnitude;
            }
        }

        return getSteeringForce(seperation);
    }

    private Vector3 alignment(GameObject[] neighbors) {
        Vector3 velocities = Vector3.zero;
        foreach (var neighbor in neighbors) {
            velocities += neighbor.GetComponent<AI_Agent>().movement.Velocity;
        }

        Vector3 averageVelocity = velocities / neighbors.Length;
        return getSteeringForce(averageVelocity);
    }

    private Vector3 getSteeringForce(Vector3 direction) {
        Vector3 desired = direction.normalized * movement.maxSpeed;
        Vector3 steer = desired - movement.Velocity;
        return Vector3.ClampMagnitude(steer, movement.maxForce);
    }
}
