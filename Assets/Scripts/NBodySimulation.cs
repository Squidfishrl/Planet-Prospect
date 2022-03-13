﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBodySimulation : MonoBehaviour {
    GravityObject[] bodies;
    static NBodySimulation instance;

    void Awake () {

        bodies = FindObjectsOfType<GravityObject> ();
        Time.fixedDeltaTime = YavorUniverse.physicsTimeStep;
        Debug.Log ("Setting fixedDeltaTime to: " + YavorUniverse.physicsTimeStep);
    }

    void FixedUpdate () {
        for (int i = 0; i < bodies.Length; i++) {
            if (!bodies[i].isRocket) {
                continue;
            }
            Vector3 acceleration = CalculateAcceleration (bodies[i].Position, bodies[i]);
            bodies[i].UpdateVelocity (acceleration, YavorUniverse.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Length; i++) {
            if (!bodies[i].isRocket) {
                continue;
            }
            bodies[i].UpdatePosition (YavorUniverse.physicsTimeStep);
        }

    }

    public static Vector3 CalculateAcceleration (Vector3 point, GravityObject ignoreBody = null) {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.bodies) {
            if (body != ignoreBody) {
                float sqrDst = (body.Position - point).sqrMagnitude;
                Vector3 forceDir = (body.Position - point).normalized;
                acceleration += forceDir * YavorUniverse.gravitationalConstant * body.mass / sqrDst;
            }
        }

        return acceleration;
    }

    public static GravityObject[] Bodies {
        get {
            return Instance.bodies;
        }
    }

    static NBodySimulation Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<NBodySimulation> ();
            }
            return instance;
        }
    }
}