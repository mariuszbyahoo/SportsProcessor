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
    public TransformerChain<RegressionPredictionTransformer<FastForestRegressionModelParameters>> TrainModel(List<double> preparedDataForMLModelTraining) 
    {
        // Create ML.NET context
        var mlContext = new MLContext();

        // Load training data from list
        IDataView trainingData = mlContext.Data.LoadFromEnumerable(_preparator.CreateTrainingData(preparedDataForMLModelTraining));

        // Define the pipeline (Random Forest)
        var pipeline = mlContext.Transforms.Concatenate("Features", nameof(HeartRateData.PreviousTicks))
            .Append(mlContext.Regression.Trainers.FastForest(labelColumnName: nameof(HeartRateData.MedianNextTicks),
                                                            featureColumnName: "Features"));

        // Train the model
        var model = pipeline.Fit(trainingData);
        return model;
    }
}
