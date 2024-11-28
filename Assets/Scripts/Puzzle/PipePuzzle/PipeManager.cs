using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Puzzle.PipePuzzle
{
    [Serializable]
    public class PipeConfiguration
    {
        public List<PipeImageConfiguration> pipesImages;

        public Sprite GetImage(Pipe.PipeType type)
        {
            return pipesImages.Find(element => element.type == type)?.image;
        }

        public PipeImageConfiguration GetRandomImage()
        {
            return pipesImages[UnityEngine.Random.Range(0, pipesImages.Count)];
        }
        
    }

    [Serializable]
    public class PipeImageConfiguration
    {
        public Pipe.PipeType type;
        public Sprite image;
    }
    
    public class PipeManager : MonoBehaviour
    {
        public static PipeManager Instance;
        
        public PipeConfiguration configuration;
        public List<Pipe> pipes = new List<Pipe>();
        public Pipe pipePrefab;
        public Coordinate2D gridSize;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            Pipe newPipe = Instantiate(pipePrefab, transform);
            Sprite newSprite = configuration.GetImage(Pipe.PipeType.GPipe);
            Coordinate2D newCoordinate = MatrixUtilities.GetCoordinateByIndex(0, gridSize);
            newPipe.Initialize(newCoordinate, Pipe.PipeType.GPipe, 0, newSprite);
            pipes.Add(newPipe);
            
            for (int i = 1; i < gridSize.Size(); i++)
            {
                Pipe pipe = Instantiate(pipePrefab, transform);
                Sprite sprite = configuration.GetImage(Pipe.PipeType.LPipe);
                Coordinate2D coordinate = MatrixUtilities.GetCoordinateByIndex(i, gridSize);
                pipe.Initialize(coordinate, Pipe.PipeType.LPipe, 0, sprite);
                pipes.Add(pipe);
            }
            
            Coordinate2D startCoordinate = MatrixUtilities.GetCoordinateByIndex(0, gridSize);
            Pipe startPipe = GetPipe(startCoordinate);
            startPipe.ChangeState(true);
        }

        public void ChangePipes(Coordinate2D currentCoordinate, int[] pipeExits, bool isActive)
        {
            Pipe currentPipe = GetPipe(currentCoordinate);
            Coordinate2D[] pipeNeighbors = VectorUtilities.NeighborsFromVector(currentCoordinate, pipeExits, gridSize);
            foreach (Coordinate2D coordinate2D in pipeNeighbors)
            {
                Pipe pipe = pipes[MatrixUtilities.GetIndexByCoordinate(coordinate2D, gridSize)];
                if (pipe.State != isActive && pipe.IsPipeConnected(currentPipe))
                {
                    pipe.ChangeState(isActive);
                }
            }
        }

        public Pipe GetPipe(Coordinate2D coordinate)
        {
            Pipe pipe = pipes[MatrixUtilities.GetIndexByCoordinate(coordinate, gridSize)];
            return pipe;
        }
        
    }
}