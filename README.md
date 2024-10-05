# SportsProcessor

## Important note
Don't know why, but I am guessing what the coverlet.collector started on some point working wrong. Or I started to use it in a bad way, netherless, in the middle of my work regarding test coverage (right after creating this Readme, there's visible point in git history), tests coverage dropped from above 80 % to below 50%, then I added three more test classes - CommaSeparatedStringToListConverterTests, DataLoaderTests and ListExtensionsTests as well as I added few tersts within SportsDataProcessorTests. Still, test coverage stayed aroung 54% when it comes to lines coverage. Branches coverage stayed in place (after adding many tests!)
Because of the above reason, I am ignoring the low result coming out of the library, and I am stepping into the **bonus task**.

In order to ensure above 80% of tests coverage I am using coverlet.collector.

## Running tests
1. build SportsProcessor.Tests
2. using terminal, run dotnet test --collect "XPLat Code Coverage"

## Inspecting code coverage by tests
1. open SportsProcessor.Tests.TestResults folder using Explorer
2. Inspect line number 2
   ```
   <coverage line-rate="0.9508" branch-rate="0.8332999999999999" version="1.9" timestamp="1728129803" lines-covered="116" lines-valid="122" branches-covered="20" branches-valid="24">
   ```

   * line-rate is percentage of lines covered with tests (1 is 100%, 95% in the above case)
   * branch-rate is percentage of code's logic covered with tests (83,3% in the above case) - logic covered I meant when if/else statement present, and only if covered with tests will result in 0.5 branch-rate
