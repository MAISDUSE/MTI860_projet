using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using Joint = Windows.Kinect.Joint;

public class MyBodySourceView2 : MonoBehaviour
{
    public GameObject mSpineShoulder;
    public GameObject mHandLeft;
    public GameObject mHandRight;
    public GameObject mFootLeft;
    public GameObject mFootRight;
    public GameObject mKneeLeft;
    public GameObject mKneeRight;
    public GameObject mShoulderLeft;
    public GameObject mShoulderRight;

    public BodySourceManager mBodySourceManager;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private List<JointType> _joints = new List<JointType>
    {
        JointType.HandLeft,
        JointType.HandRight,
        JointType.FootLeft,
        JointType.FootRight,
        JointType.SpineShoulder,
        JointType.ShoulderLeft,
        JointType.ShoulderRight,
        JointType.KneeLeft,
        JointType.KneeRight
        

    };
    private Dictionary<JointType, GameObject> _BoneMap = new Dictionary<JointType, GameObject>();

    /*    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
        {
            { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
            { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
            { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
            { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },

            { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
            { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
            { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
            { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },

            { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
            { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
            { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
            { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
            { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
            { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },

            { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
            { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
            { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
            { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
            { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
            { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },

            { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
            { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
            { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
            { Kinect.JointType.Neck, Kinect.JointType.Head },
        };
    */
    void Update()
    {
        #region Get Kinect Data
        Body[] data = mBodySourceManager.GetData();
        if (data == null)
        {
            return;
        }


        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                trackedIds.Add(body.TrackingId);
            }
        }
        #endregion

        #region Delete Kinect Bodies
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        // First delete untracked bodies
        foreach (ulong trackingId in knownIds)
        {
            if (!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }
        #endregion

        #region Create Kinect Bodies
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                if (!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }

                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
        #endregion
    }

    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        this._BoneMap.Add(_joints[0], mHandLeft);
        this._BoneMap.Add(_joints[1], mHandRight);
        this._BoneMap.Add(_joints[2], mFootLeft);
        this._BoneMap.Add(_joints[3], mFootRight);
        this._BoneMap.Add(_joints[4], mSpineShoulder);
        this._BoneMap.Add(_joints[5], mShoulderLeft);
        this._BoneMap.Add(_joints[6], mShoulderRight);
        this._BoneMap.Add(_joints[7], mKneeLeft);
        this._BoneMap.Add(_joints[8], mKneeRight);

       

        foreach (JointType joint in _BoneMap.Keys)
        {
            _BoneMap[joint].transform.parent = body.transform;
        }

        /*foreach (JointType joint in _joints)
        {
            GameObject i;
            string name = "m" + joint.ToString();
            i = GameObject.Find(name);
            //i = Instantiate(mJointObject);
            //i.name = joint.ToString();

            i.transform.parent = body.transform;

        }*/

        return body;

    }

    private void RefreshBodyObject(Body body, GameObject bodyObject)
    {
        foreach (JointType _joint in _joints)
        {
            Joint sourcePoint = body.Joints[_joint];
            Vector3 targetPosition = GetVector3FromJoint(sourcePoint);

            Transform jointObject = bodyObject.transform.Find(_joint.ToString());
            jointObject.position = targetPosition;
        }
    }


    private static Vector3 GetVector3FromJoint(Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
