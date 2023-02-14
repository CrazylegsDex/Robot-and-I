// This script controls a Camera
//
// Author: Robot and I Team
// Last modification date: 02-06-2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FocalPoint
{
    public class Focal_Point_Camera_Control: MonoBehaviour
    {
        // Public variables - Inspector View modifiable
        public Transform curCam;

        // Private variables
        private Transform tf;
        private Vector3 camZDepth;

        // Awake is called after all objects are initialized. Called in a random order with the rest.
        private void Awake()
        {
            // Get the elements in the current sprites Rigidbody2D and SpriteRenderer
            tf = GetComponent<Transform>();
            camZDepth = new Vector3(0f, 0f, -10f);
        }

        // Start is called before the first frame update
        void Start()
        {
            //simple camera set, sets camera to
            curCam.position = tf.position + camZDepth;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            curCam.position = tf.position + camZDepth;
        }
    }
}
