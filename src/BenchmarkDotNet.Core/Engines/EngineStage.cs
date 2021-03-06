﻿using System;
using BenchmarkDotNet.Characteristics;
using BenchmarkDotNet.Horology;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;

namespace BenchmarkDotNet.Engines
{
    public class EngineStage
    {
        private readonly IEngine engine;

        protected EngineStage(IEngine engine)
        {
            this.engine = engine;
        }

        protected Job TargetJob => engine.TargetJob;
        protected AccuracyMode TargetAccuracy => TargetJob.Accuracy;
        protected IClock TargetClock => engine.Resolver.Resolve(TargetJob.Infrastructure.Clock);
        protected IResolver Resolver => engine.Resolver;

        protected Measurement RunIteration(IterationMode mode, int index, long invokeCount, int unrollFactor)
        {
            if (invokeCount % unrollFactor != 0)
                throw new ArgumentOutOfRangeException($"InvokeCount({invokeCount}) should be a multiple of UnrollFactor({unrollFactor}).");
            return engine.RunIteration(new IterationData(mode, index, invokeCount, unrollFactor));
        }

        protected void WriteLine() => engine.WriteLine();

        protected void WriteLine(string line) => engine.WriteLine(line);
    }
}