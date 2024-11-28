using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

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
            GPipe,
        }

        [Serializable]
        public enum Degrees
        {
            D0 = 0,
            D90 = 1,
            D180 = 2,
            D270 = 3,
        }

        private static readonly Dictionary<PipeType, int[]> Pipes = new ()
        {
            { PipeType.TPipe , new[]{0, 1, 1, 1}},
            { PipeType.LPipe , new[]{0, 0, 1, 1}},
            { PipeType.SPipe , new[]{0, 1, 0, 1}},
            { PipeType.GPipe , new[]{1, 0, 1, 0}},
        };

        [Header("Pipe Settings")]
        [SerializeField] private PipeType pipeType;
        [SerializeField] private Degrees pipeDegrees;
        [SerializeField] private int[] pipeCurrentExit;
        [SerializeField] private int[] pipeLastExit;
        [SerializeField] private Coordinate2D coordinates;
        [SerializeField] private bool isActive;
        
        public bool State => isActive;
        
        public int[] PipeExit => pipeCurrentExit;
        
        [Header("UI")]
        [SerializeField] private Image pipeImage;
        [SerializeField] private Button pipeButton;
        
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
            pipeLastExit = pipeCurrentExit;
            pipeCurrentExit = VectorUtilities.RotateVector(Pipes[pipeType], (int) pipeDegrees * 90);
            pipeImage.rectTransform.Rotate(0, 0, -90);
            
            
            PipeManager.Instance.ChangePipes(coordinates, pipeCurrentExit, isActive);
        }

        public void Initialize(Coordinate2D coord, PipeType type, Degrees degrees, Sprite sprite)
        {
            coordinates = coord;
            pipeType = type;
            pipeDegrees = degrees;
            
            pipeCurrentExit = pipeLastExit = VectorUtilities.RotateVector(Pipes[pipeType], (int) pipeDegrees * 90);
            isActive = false;

            pipeImage.sprite = sprite;
        }
        
        public void ChangeState(bool active)
        {
            isActive = active;
            
            pipeImage.color = isActive ? Color.green : Color.white;
            int[] pipeExit = active ? pipeCurrentExit : pipeLastExit;
            
            PipeManager.Instance.ChangePipes(coordinates, pipeExit, active);
        }
        
    }
}