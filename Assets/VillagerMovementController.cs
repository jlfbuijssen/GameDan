using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillagerMovementController : MonoBehaviour {
    [SerializeField]
    private NavMeshAgent villagerNavMeshAgent;
    List<Vector3> villagerDestinationTargets = new List<Vector3>();

    [SerializeField]
    private float villagerMaxSpeed;

    private void OnValidate() {
        villagerNavMeshAgent.speed = villagerMaxSpeed;
    }




    // Update is called once per frame
    void FixedUpdate() {
        HandlePlayerInput();
        HandleVillagerMovement();
    }

    private void HandlePlayerInput() {
        if (Input.GetKey(KeyCode.Mouse1)) {
            Ray rayCastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(rayCastRay, out hit, 100f);
            Debug.DrawLine(rayCastRay.origin, hit.point, Color.red);
            if (!Input.GetKey(KeyCode.LeftShift)) {
                Debug.Log("Shift not pressed, resetting target list");
                villagerDestinationTargets.Clear();
                villagerNavMeshAgent.ResetPath();
            }
            villagerDestinationTargets.Add(hit.point);
        }
    }

    private void HandleVillagerMovement() {
        if (!villagerNavMeshAgent.hasPath) {
            Debug.Log("Arrived at position, getting new destination off the queue.");
            if (villagerDestinationTargets.Count > 0 ) {
                villagerNavMeshAgent.SetDestination(villagerDestinationTargets[0]);
                villagerDestinationTargets.RemoveAt(0);
            }
        }
    }
}
