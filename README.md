# SportsProcessor

# Main task
The main task is to take the input data, load it into the library, and then process it where the output will be one consolidated JSON containing data without outliers and "dirty data" like records kind of `null`. 

Main task is being solved by the code within `SportsProcessor` project

## Running unit tests
1. build SportsProcessor.Tests
2. using terminal, run dotnet test --collect "XPLat Code Coverage"

## Inspecting code coverage by tests
1. open SportsProcessor.Tests.TestResults folder using Explorer
2. Inspect line number 2
   ```
   <coverage line-rate="0.9508" branch-rate="0.8332999999999999" version="1.9" timestamp="1728129803" lines-covered="116" lines-valid="122" branches-covered="20" branches-valid="24">
   ```

   * line-rate is percentage of lines covered with tests (1 is 100%, 95% in the above case)
   * branch-rate is percentage of code's logic covered with tests (83,3% in the above case) - by saying 'logic covered' I meant when if/else statement present, and only if covered with tests will result in 0.5 branch-rate

# Bonus Task

## Projects within bonus task
1. SportsProcessor.Statistics - contains classes and code related to data pre-processing and training the model.
2. SportsProcessor.Statistics.Tests - Due to the fact I were unable to solve exception issues mentioned below I got rid of it as there were no tests, rather I had used it as a mere executable to run my code and inspect it's outcome.

I were able to prepare the dataset using `StatsPreProcessor` class - it contains all of the code necessary to perform first part of bonus task as mentioned in the task : 

**Your goal is to design, implement, test and document a methodology for pre-processing and modelling of the heart rate measurements within a lap. The pre-processing part should cover outlier identification and cleaning. The initial recording rate is set to `5`, whereas each observation is a median aggregate of the 5 tick heart rate measurments. You need to reverse the aggregation step and backward interpolate the observations in a way, that you end up with 5 * (n-1) heart rate measurements with the corresponding recording rate of `1`, where n denotes the initial number of observations.**

In order to predict future values I selected `random forest` as the best model considering it's simplicity and the fact that input data are not linear (some readings are for example starting from below 80, going through above 100 and finishing at below 90. 
Further on, when I dive deeper into the task I got into an exception in `ModelTrainer.TrainAndEvaluateModel()` method, it doesn't matter that I am passing in a set of arrays where everyone of them contains 10 double elements, I am getting an exception:
```
An exception of type 'System.ArgumentOutOfRangeException' occurred in Microsoft.ML.Data.dll but was not handled in user code: 'Schema mismatch for feature column 'Features': expected Vector<Single>, got VarVector<Single>'
```
In the line:
```
        var model = pipeline.Fit(trainingDataView);
```
- Even when I got into the only idea of a cause for such an exception (maybe fractionals?) I found out that this is not the case as I am getting the same exception **even if** instead of code
```
        var trainingData = _preparator.TakeDataChunkForMLProcessing(firstHalf);
```
I will use
```
        var trainingData = _preparator.TakeDataChunkForMLProcessing(firstHalf.Select(d => double.Round(d)).ToList());
```
So, this is how I ran out of any ideas about causes of this error, therefore I am just getting rid of my SportsProcessor.Statistics.Tests project - as I am considering more as a code smell than a real value, still it is possible to be recreated out of the git history (last commit will be delete of this project)
