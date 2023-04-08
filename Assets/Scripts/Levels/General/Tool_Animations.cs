using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class Tool_Animations : MonoBehaviour
    {
        public enum ToolStates
        {
            Toolless,
            Drop,
            Hold,
            Miss,
            Reach,
            Throw
        } // Keeps track of all the animation states
        static ToolStates curToolState;

        private static Transform toolTf;
        private static Animator toolAn;
        private static GameObject bitOb;
        private static Transform bitTf;
        private static Rigidbody2D bitRb;
        private static GameObject chec;    // Game Object for the
        private static Transform checTf;   // transform for the 
        private static GameObject heldObj;     // contains the object that bit has "actioned" (for now just grabbed)
        private static Transform heldObjTf;    // the transform for
        private static BoxCollider2D heldObjBc;
        private static Rigidbody2D heldObjRb;
        private static Vector3 heldOffset = new Vector3(0f, 20f, 0f);
        private static Vector3 releOffsetR = new Vector3(10f, 15f, 0f);
        private static Vector3 releOffsetL = new Vector3(-10f, 15f, 0f);
        private static bool aniLockT = false;   // if true no new Tool animation will start
        //private static float tranSpeed = 1f;
        private static bool held = false;
        private static bool isRight;

        static ToolStates CurToolState
        {
            set
            {
                if (!aniLockT)
                {
                    curToolState = value;
                    switch (curToolState)
                    {
                        case ToolStates.Toolless:
                            toolAn.Play("Toolless");
                            Platformer_Movement.PlayBit();
                            break;
                        case ToolStates.Drop:
                            toolAn.Play("Drop");
                            aniLockT = true;
                            Platformer_Movement.PauseBit();
                            break;
                        case ToolStates.Hold:
                            toolAn.Play("Hold");
                            Platformer_Movement.PlayBit();
                            break;
                        case ToolStates.Miss:
                            toolAn.Play("Miss");
                            aniLockT = true;
                            Platformer_Movement.PauseBit();
                            break;
                        case ToolStates.Reach:
                            toolAn.Play("Reach");
                            aniLockT = true;
                            Platformer_Movement.PauseBit();
                            break;
                        case ToolStates.Throw:
                            toolAn.Play("Throw");
                            aniLockT = true;
                            Platformer_Movement.PauseBit();
                            break;
                    }
                }
            }
        }  // sets what actions will be performed for each animation change

        void Awake()
        {
            toolTf = GetComponent<Transform>();
            toolAn = GetComponent<Animator>();
            bitOb = transform.parent.gameObject;
            bitTf = bitOb.GetComponent<Transform>();
            chec = GameObject.FindWithTag("Checker");
            checTf = chec.GetComponent<Transform>();
        }

        public static void ToolAction()
        {
            //isAct = true;
            // if Bit is not holding something, grab something
            if (CheCurToolAni("Toolless"))
            {
                CurToolState = ToolStates.Reach;
            }
            // if Bit is already holding something throw
            else if (CheCurToolAni("Hold"))
            {
                CurToolState = ToolStates.Drop;
            }
        }

        public static void ToolUpdate(bool Right)
        {
            isRight = Right;
            if (held)
            {
                heldObjTf.position = heldOffset + bitTf.position;
            }
            if (isRight)
            {
                toolTf.localScale = new Vector3(2f, 2f, 0f);
            }
            else
            {
                toolTf.localScale = new Vector3(-2f, 2f, 0f);
            }
        }

        /*
        public static void GrabUpdate()
        {
            while (held)
            {
                float step = tranSpeed * Time.deltaTime;
                heldObjTf.position = Vector3.MoveTowards(heldObjTf.position, bitTf.position + heldOffset, step);
            }
        }

        public static void HeldUpdate()
        {
            heldObjTf.position = bitTf.position + heldOffset;
        }

        public static void ReleUpdate()
        {
            while (held)
            {
                float step = tranSpeed * Time.deltaTime;
                heldObjTf.position = Vector3.MoveTowards(heldObjTf.position, checTf.position + releOffset, step);
            }
        }
        */

        public static bool CheCurToolAni(string name)
        {
            return toolAn.GetCurrentAnimatorStateInfo(0).IsName(name);
        }

        // check for Grabbable. Then finish animation, or play a miss animation
        private void ReacAniMid()
        {
            if (Bit_Focus.IsGrabbable())
            {
                heldObj = Bit_Focus.WhatGrab();
                heldObjTf = heldObj.GetComponent<Transform>();
                heldObjBc = heldObj.GetComponent<BoxCollider2D>();
                heldObjRb = heldObj.GetComponent<Rigidbody2D>();
                heldObjRb.bodyType = RigidbodyType2D.Kinematic;
                heldObjBc.enabled = false;
                if (isRight)
                {
                    heldObjTf.position = releOffsetR + bitTf.position;
                }
                else
                {
                    heldObjTf.position = releOffsetL + bitTf.position;
                }
                //GrabUpdate();
                held = true;
            }
            else
            {
                aniLockT = false;
                CurToolState = ToolStates.Miss;
            }
        }

        // release held object and finish up with it
        private void RelMid()
        {
            if (isRight)
            {
                heldObjTf.position = releOffsetR + checTf.position;
            }
            else
            {
                heldObjTf.position = releOffsetL + checTf.position;
            }
            heldObjBc.enabled = true;
            heldObjRb.bodyType = RigidbodyType2D.Dynamic;
            //heldObjRb.velocity = new Vector2(bitRb.velocity.x, 0f);
            //ReleUpdate();
            held = false;
        }

        // finish with hold
        private void ReacAniEnd()
        {
            aniLockT = false;
            CurToolState = ToolStates.Hold;
        }

        // finish with no tool
        private void EmptAniEnd()
        {
            aniLockT = false;
            CurToolState = ToolStates.Toolless;
        }
    }
}
