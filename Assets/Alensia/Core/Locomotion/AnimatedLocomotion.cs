﻿using System;
using Alensia.Core.Animation;
using Alensia.Core.Common;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Alensia.Core.Locomotion
{
    public abstract class AnimatedLocomotion : Locomotion, IAnimatable
    {
        public bool UseRootMotionForMovement = true;

        public bool UseRootMotionForRotation = false;

        public MovementVariables MovementVariables { get; private set; }

        public RotationVariables RotationVariables { get; private set; }

        public Animator Animator { get; private set; }

        protected AnimatedLocomotion(
            Animator animator,
            Transform transform) : this(new Settings(), animator, transform)
        {
        }

        [Inject]
        protected AnimatedLocomotion(
            Settings settings,
            Animator animator,
            Transform transform) : base(transform)
        {
            Assert.IsNotNull(settings, "settings != null");
            Assert.IsNotNull(animator, "animator != null");

            MovementVariables = settings.MovementVariables;
            RotationVariables = settings.RotationVariables;

            Assert.IsNotNull(MovementVariables, "settings.MovementVariables != null");
            Assert.IsNotNull(RotationVariables, "settings.RotationVariables != null");

            Animator = animator;
        }

        protected override void UpdateVelocity(Vector3 velocity)
        {
            Animator.SetBool(MovementVariables.Moving, velocity.magnitude > 0);

            Animator.SetFloat(MovementVariables.SpeedRight, velocity.x);
            Animator.SetFloat(MovementVariables.SpeedUp, velocity.y);
            Animator.SetFloat(MovementVariables.SpeedForward, velocity.z);
        }

        protected override void UpdateRotation(Vector3 angularVelocity)
        {
            Animator.SetBool(RotationVariables.Turning, angularVelocity.magnitude > 0);

            Animator.SetFloat(RotationVariables.SpeedPitch, angularVelocity.x);
            Animator.SetFloat(RotationVariables.SpeedYaw, angularVelocity.y);
            Animator.SetFloat(RotationVariables.SpeedRoll, angularVelocity.z);
        }

        [Serializable]
        public class Settings : IEditorSettings
        {
            public MovementVariables MovementVariables = new MovementVariables();

            public RotationVariables RotationVariables = new RotationVariables();
        }
    }

    [Serializable]
    public class MovementVariables : IEditorSettings
    {
        public string Moving = "Moving";

        public string SpeedForward = "SpeedForward";

        public string SpeedRight = "SpeedRight";

        public string SpeedUp = "SpeedUp";
    }

    [Serializable]
    public class RotationVariables : IEditorSettings
    {
        public string Turning = "Turning";

        public string SpeedPitch = "SpeedPitch";

        public string SpeedRoll = "SpeedRoll";

        public string SpeedYaw = "SpeedYaw";
    }
}