using System.Linq;
using HurricaneVR.Framework.Core.Player;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class TeleporterScript : MonoBehaviour
    {
        public HVRTeleporter Teleporter { get; set; }
        public Transform SpawnPoint;

        public void Start()
        {
            Teleporter = GameObject.FindObjectOfType<HVRTeleporter>();
        }

        public void Teleport()
        {
            if (Teleporter && SpawnPoint)
            {
                Teleporter.Teleport(SpawnPoint.position, SpawnPoint.forward);
            }
        }
    }
}
