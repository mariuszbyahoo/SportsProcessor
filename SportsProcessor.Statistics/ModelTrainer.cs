using System;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using SportsProcessor.Statistics.Models;

namespace SportsProcessor.Statistics;

public class ModelTrainer
{
    private readonly DataPreparator _preparator;

    public ModelTrainer(DataPreparator preparator)
    {
        _preparator = preparator;
    }
    public RegressionMetrics? TrainAndEvaluateModel(List<double> preparedDataForMLModelTraining) 
    {
        var totalCount = preparedDataForMLModelTraining.Count;
        var halfCount = totalCount / 2;

        var firstHalf = preparedDataForMLModelTraining.Take(halfCount).ToList();
        var secondHalf = preparedDataForMLModelTraining.Skip(halfCount).ToList();

        var trainingData = _preparator.TakeDataChunkForMLProcessing(firstHalf.Select(d => double.Round(d)).ToList());
        var evaluationData = _preparator.TakeDataChunkForMLProcessing(secondHalf.Select(d => double.Round(d)).ToList());
        // Create ML.NET context
        var mlContext = new MLContext();

        // Load training data from list
        IDataView trainingDataView = mlContext.Data.LoadFromEnumerable(trainingData);

        // Define the pipeline (Random Forest)
        var pipeline = mlContext.Transforms.Concatenate("Features", nameof(HeartRateData.PreviousTicks))
            .Append(mlContext.Regression.Trainers.FastForest(labelColumnName: nameof(HeartRateData.MedianNextTicks),
                                                            featureColumnName: "Features"));

        // Train the model
        /*
            * In the line Below I am getting
            Exception has occurred: CLR/System.ArgumentOutOfRangeException
            An exception of type 'System.ArgumentOutOfRangeException' occurred in Microsoft.ML.Data.dll but was not handled in user code: 'Schema mismatch for feature column 'Features': expected Vector<Single>, got VarVector<Single>'
            at Microsoft.ML.Trainers.TrainerEstimatorBase`2.CheckInputSchema(SchemaShape inputSchema)
            at Microsoft.ML.Trainers.TrainerEstimatorBase`2.GetOutputSchema(SchemaShape inputSchema)
            at Microsoft.ML.Data.EstimatorChain`1.GetOutputSchema(SchemaShape inputSchema)
            at Microsoft.ML.Data.EstimatorChain`1.Fit(IDataView input)
            at SportsProcessor.Statistics.ModelTrainer.TrainAndEvaluateModel(List`1 preparedDataForMLModelTraining) in /Users/mariuszbudzisz/Documents/Projects/SportsProcessor/SportsProcessor.Statistics/ModelTrainer.cs:line 39
            at SportsProcessor.Statistics.Tests.StatsPreProcessorTests.DebugMyPreProcessor() in /Users/mariuszbudzisz/Documents/Projects/SportsProcessor/SportsProcessor.Statistics.Tests/StatsPreProcessorTests.cs:line 40
            at System.RuntimeMethodHandle.InvokeMethod(Object target, Void** arguments, Signature sig, Boolean isConstructor)
            at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)


            Whats the most weird is the fact that the trainingDataEnumerable in PreviousTicks array
            contains ALWAYS 10 elements - so why ML.NET considers it to be VarVector?

        */
        var model = pipeline.Fit(trainingDataView);

        // Step 4: Evaluate the model using test data
        IDataView testData = mlContext.Data.LoadFromEnumerable(evaluationData);

        // Use the trained model to transform the test data
        IDataView predictions = model.Transform(testData);

        // Evaluate the model's performance
        var metrics = mlContext.Regression.Evaluate(predictions, labelColumnName: "MedianNextTicks");

        Console.WriteLine($"R-Squared: {metrics.RSquared}");
        Console.WriteLine($"RMSE: {metrics.RootMeanSquaredError}");
        Console.WriteLine($"MAE: {metrics.MeanAbsoluteError}");
        return metrics;
    }
}
