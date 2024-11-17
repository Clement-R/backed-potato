using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TitleScreen
{
    [RequireComponent(typeof(AudioSource))]
    public class GangstaPotatoAnimationController : MonoBehaviour
    {
        private AudioSource _audioSource;
        private Animator _animator;

        public float blinkMinInterval = 2;
        public float blinkMaxInterval = 4;

        public float scratchHeadMinInterval = 2;
        public float scratchHeadMaxInterval = 4;

        public float moveMouthMinInterval = 2;
        public float moveMouthMaxInterval = 4;

        private float _nextBlinkTime;
        private float _nextScratchHeadTime;
        private float _nextMoveMouthTime;
        private static readonly int TriggerBlink = Animator.StringToHash("TriggerBlink");
        private static readonly int TriggerScratchHead = Animator.StringToHash("TriggerScratchHead");
        private static readonly int TriggerMoveMouth = Animator.StringToHash("TriggerMoveMouth");

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
            _nextBlinkTime = GenerateNextBlinkTime();
            _nextScratchHeadTime = GenerateNextScratchHeadTime();
            _nextMoveMouthTime = GenerateNextMoveMouthTime();
        }

        private float GenerateNextBlinkTime()
        {
            return Time.time + Random.Range(blinkMinInterval, blinkMaxInterval);
        }

        private float GenerateNextScratchHeadTime()
        {
            return Time.time + Random.Range(scratchHeadMinInterval, scratchHeadMaxInterval);
        }

        private float GenerateNextMoveMouthTime()
        {
            return Time.time + Random.Range(moveMouthMinInterval, moveMouthMaxInterval);
        }

        private void Update()
        {
            ResetAnimatorState();

            var currentTime = Time.time;

            if (currentTime >= _nextBlinkTime)
            {
                _animator.SetBool(TriggerBlink, true);
                _nextBlinkTime = GenerateNextBlinkTime();
            }

            if (currentTime >= _nextScratchHeadTime)
            {
                _animator.SetBool(TriggerScratchHead, true);
                _nextScratchHeadTime = GenerateNextScratchHeadTime();
            }

            if (currentTime >= _nextMoveMouthTime)
            {
                _animator.SetBool(TriggerMoveMouth, true);
                _nextMoveMouthTime = GenerateNextMoveMouthTime();
            }
        }

        private void ResetAnimatorState()
        {
            var blinkState = _animator.GetCurrentAnimatorStateInfo(_animator.GetLayerIndex("Blink"));
            var scratchHeadState = _animator.GetCurrentAnimatorStateInfo(_animator.GetLayerIndex("ScratchHead"));
            var moveMouthState = _animator.GetCurrentAnimatorStateInfo(_animator.GetLayerIndex("MoveMouth"));

            if (!blinkState.IsName("Idle"))
            {
                _animator.SetBool(TriggerBlink, false);
            }

            if (!scratchHeadState.IsName("Idle"))
            {
                _animator.SetBool(TriggerScratchHead, false);
            }

            if (!moveMouthState.IsName("Idle"))
            {
                _animator.SetBool(TriggerMoveMouth, false);
            }
        }

        private void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}