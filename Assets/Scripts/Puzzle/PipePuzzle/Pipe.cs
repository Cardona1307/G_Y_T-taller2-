using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.PipePuzzle
{
    public class Pipe : MonoBehaviour
    {
        [Serializable]
        public enum PipeType
        {
            TPipe,
            LPipe,
            SPipe,
        }

        [Serializable]
        public enum Degrees
        {
            D0 = 0,
            D90 = 1,
            D180 = 2,
            D270 = 3,
        }
        
        public static Dictionary<PipeType, int[]> pipes = new ()
        {
            { PipeType.TPipe , new int[]{0, 1, 1, 1}},
            { PipeType.LPipe , new int[]{0, 0, 1, 1}},
            { PipeType.SPipe , new int[]{0, 1, 0, 1}}
        };

        [Header("Pipe Settings")]
        public PipeType pipeType;
        public Degrees pipeDegrees;
        public int[] pipeVector;
        public int[] pipeActivationVector;
        public bool isActive;
        
        [Header("UI")]
        public Image pipeImage;
        public Button pipeButton;

        private void OnEnable()
        {
            pipeButton.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            pipeButton.onClick.RemoveAllListeners();
        }

        private void OnClick()
        {
            pipeDegrees = (Degrees) ( (int) (pipeDegrees + 1) % 4 );
            pipeVector = VectorFunctions.RotateVector(pipes[pipeType], (int) pipeDegrees * 90);
            pipeImage.rectTransform.Rotate(0, 0, -90);
            isActive = VectorFunctions.IsVectorActive(pipeVector, pipeActivationVector);
        }

        public void Initialize(PipeType type, Degrees degrees, Sprite sprite)
        {
            pipeType = type;
            pipeDegrees = degrees;
            
            pipeVector = VectorFunctions.RotateVector(pipes[pipeType], (int) pipeDegrees * 90);
            pipeActivationVector = new []{0, 0, 0, 0};
            isActive = false;

            pipeImage.sprite = sprite;
        }
        
    }
}