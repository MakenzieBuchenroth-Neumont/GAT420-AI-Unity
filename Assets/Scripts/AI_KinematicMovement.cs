using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_KinematicMovement : AI_Movement {
    public override void applyForce(Vector3 force) {
        Acceleration += force;
    }

    public override void moveTowards(Vector3 target) {
        Vector3 direction = (target - transform.position).normalized;
        applyForce(direction * maxForce);
    }

    public override void stop() {
        Velocity = Vector3.zero;
    }

    public override void resume() {
        //
    }

    void LateUpdate() {
        Velocity += Acceleration * Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity, maxSpeed);
        //Velocity = Velocity.ClampMagnitude(minSpeed, maxSpeed);
        transform.position += Velocity * Time.deltaTime;

        if (Velocity.sqrMagnitude > 0.1f) {
            transform.rotation = Quaternion.LookRotation(Velocity);
        }

        Acceleration = Vector3.zero;
    }
}
