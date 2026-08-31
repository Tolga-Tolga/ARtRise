using System;
using System.Collections.Generic;
using UnityEngine;

namespace OpenCVForUnity.UnityUtils.Helper
{
    /// <summary>
    /// The ARHelper class manages ARCamera and ARGameObjects, automating the calculation of ARMatrix and updating the Transform of the specified UpdateTarget.
    /// </summary>
    /// <remarks>
    /// - Calculates the ARMatrix of registered ARGameObjects based on the settings of ARCamera and ARGameObjects.
    /// - Updates the Transform of the specified UpdateTarget (ARCamera or ARGameObject) using the calculated ARMatrix.
    /// - Allows control over automatic execution in LateUpdate() via boolean values.
    /// - Provides functionality to reset ImagePoints and ObjectPoints.
    /// </remarks>
    [Obsolete("This class is deprecated. Use OpenCVForUnity.UnityIntegration.Helper.AR.ARHelper class instead.")]
    public class ARHelper : MonoBehaviour
    {
        /// <summary>
        /// The type of target to update.
        /// </summary>
        public enum ARUpdateTarget
        {
            ARGameObject,
            ARCamera
        }

        [SerializeField, Tooltip("Registered AR camera.")]
        protected ARCamera _arCamera;

        /// <summary>
        /// Registered AR camera.
        /// </summary>
        public virtual ARCamera ARCamera
        {
            get => _arCamera;
            set => _arCamera = value;
        }

        [SerializeField, Tooltip("Registered AR game objects.")]
        protected List<ARGameObject> _arGameObjects;

        /// <summary>
        /// Registered AR game objects.
        /// </summary>
        public virtual List<ARGameObject> ARGameObjects
        {
            get => _arGameObjects;
            set => _arGameObjects = value;
        }

        [Space(10)]

        [SerializeField, Tooltip("Specifies the GameObject whose Transform will be updated by the UpdateTransform() method.")]
        protected ARUpdateTarget _updateTarget;

        /// <summary>
        /// Specifies the GameObject whose Transform will be updated by the UpdateTransform() method.
        /// </summary>
        public virtual ARUpdateTarget UpdateTarget
        {
            get => _updateTarget;
            set => _updateTarget = value;
        }

        [Space(10)]

        [SerializeField, Tooltip("If true, the CalculateARMatrix() method is automatically called in LateUpdate(). If false, it must be called manually.")]
        protected bool _calculateARMatrixInLateUpdate = true;

        /// <summary>
        /// If true, the CalculateARMatrix() method is automatically called in LateUpdate(). If false, it must be called manually.
        /// </summary>
        public virtual bool CalculateARMatrixInLateUpdate
        {
            get => _calculateARMatrixInLateUpdate;
            set => _calculateARMatrixInLateUpdate = value;
        }

        [SerializeField, Tooltip("If true, the UpdateTransform() method is automatically called in LateUpdate(). If false, it must be called manually.")]
        protected bool _updateTransformInLateUpdate = true;

        /// <summary>
        /// If true, the UpdateTransform() method is automatically called in LateUpdate(). If false, it must be called manually.
        /// </summary>
        public virtual bool UpdateTransformInLateUpdate
        {
            get => _updateTransformInLateUpdate;
            set => _updateTransformInLateUpdate = value;
        }

        /// <summary>
        /// Indicates whether this instance has been initialized.
        /// </summary>
        protected bool _hasInitDone = false;

        /// <summary>
        /// Calls CalculateARMatrix() and UpdateTransform() in LateUpdate().
        /// </summary>
        protected virtual void LateUpdate()
        {
            //Debug.Log("LateUpdate");

            if (_calculateARMatrixInLateUpdate)
                CalculateARMatrix();
            if (_updateTransformInLateUpdate)
                UpdateTransform();
        }

        /// <summary>
        /// Initializes resources and sets the initial values.
        /// </summary>
        public virtual void Initialize()
        {
            _hasInitDone = true;
        }

        /// <summary>
        /// Disposes the ARGameObject.
        /// </summary>
        public virtual void Dispose()
        {
            _hasInitDone = false;
        }

        /// <summary>
        /// Calculates the ARMatrix for ARGameObjects based on the settings of the registered ARCamera and ARGameObjects.
        /// </summary>
        public virtual void CalculateARMatrix()
        {
            //Debug.Log("CalculateARMatrix");

            if (!_hasInitDone)
            {
                if (!_calculateARMatrixInLateUpdate)
                    Debug.LogWarning("ARHelper has not been initialized.");
                return;
            }

            if (_arCamera == null) return;

            foreach (var item in _arGameObjects)
            {
                if (item == null) continue;
                item.CalculateARMatrix(this);
            }
        }

        /// <summary>
        /// Updates the Transform of the specified UpdateTarget using the ARMatrix of ARGameObjects.
        /// </summary>
        public virtual void UpdateTransform()
        {
            //Debug.Log("UpdateTransform");

            if (!_hasInitDone)
            {
                if (!_updateTransformInLateUpdate)
                    Debug.LogWarning("ARHelper has not been initialized.");
                return;
            }

            if (_arCamera == null) return;

            foreach (var item in _arGameObjects)
            {
                if (item == null) continue;
                item.UpdateTransform(this);
            }
        }

        /// <summary>
        /// Resets the ImagePoints and ObjectPoints of ARGameObjects.
        /// </summary>
        public virtual void ResetARGameObjectsImagePointsAndObjectPoints()
        {
            if (!_hasInitDone)
            {
                Debug.LogWarning("ARHelper has not been initialized.");
                return;
            }

            foreach (var item in _arGameObjects)
            {
                if (item == null) continue;
                item.ResetImagePointsAndObjectPoints();
            }
        }
    }
}
