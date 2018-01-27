using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class IsometricCamera : MonoBehaviour {
    public float _angle;
    public float _angle2;
    public float _translate;
    //public float _scale;
    private Camera _camera;
	// Update is called once per frame
	void Update ()
    {

        _camera = GetComponent<Camera>();
        float orthoSize = _camera.orthographicSize;
        //Debug.Log()
        _camera.ResetProjectionMatrix();
        Matrix4x4 pm = _camera.projectionMatrix;


        float angleScale = 1.0f / Mathf.Cos(_angle * Mathf.Deg2Rad);
        float translate = - 2.0f * Mathf.Sin(_angle * Mathf.Deg2Rad) * orthoSize;


        float angleScale2 = 1.0f / Mathf.Cos(_angle2 * Mathf.Deg2Rad);
        float translate2 = 2.0f * Mathf.Sin(_angle2 * Mathf.Deg2Rad) * orthoSize;


        _camera.projectionMatrix = pm * Matrix4x4.TRS(new Vector3(0, translate, 0), Quaternion.Euler(_angle, 0, 0), new Vector3(1, angleScale, 1)) * Matrix4x4.TRS(new Vector3(translate2, 0, 0), Quaternion.Euler(0, _angle2, 0), new Vector3(angleScale2, 1, 1));

        //= Matrix4x4.Ortho(_camera.rect.left, _camera.rect.right, _camera.rect.bottom, _camera.rect.top, _camera.near, _camera.far);



    }

    private void OnDisable()
    {
        _camera.ResetProjectionMatrix();
    }
}
