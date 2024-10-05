# SportsProcessor

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
