using System;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    [Serializable]
    public class PipeImageConfiguration
    {
        public Pipe.PipeType type;
        public Sprite image;
    }
    
    public class PipeManager : MonoBehaviour
    {
        public PipeConfiguration configuration;
        public List<Pipe> pipes = new List<Pipe>();
        public Pipe pipePrefab;
        public int amount;
        
        private void Start()
        {
            for (int i = 0; i < amount; i++)
            {
                Pipe pipe = Instantiate(pipePrefab, transform);
                Sprite sprite = configuration.GetImage(Pipe.PipeType.LPipe);
                pipe.Initialize(Pipe.PipeType.LPipe, Pipe.Degrees.D0, sprite);
            }
        }
    }
}