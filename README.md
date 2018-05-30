# Knapsack-Problems
This is research work about Knapsack Problems (0-1 Knapsack Problem, Unbounded Knapsack Problem) using several algorithms

## Project structure
1. app/ - is a folder containing libs of algorithms and a console application for creating reports 
2. results/ - is a folder containing reports as Excel spreadsheets with the results of experiments
3. docs/ - is a folder containing the theory of research work 

## Knapsack-Problems application
You can use the following algorithms from the list below in the form of api calls from ExactAlgorithms / GeneticAlgorithm lib
### Implemented algorithms  
1. Genetic algorithm with different combinations  
2. Exact algorithms:  
  2.1. Branch And Bound with DFS and BFS traversals  
  2.2. Dynamic Programming Algorithm with Direct(Table) and Recurrent approaches  
  2.3. Branch And Bound for UKP with U3 Total Upper Bound  
  2.4. Classical Dynamic Programming for UKP  
  2.4. Efficient Dynamic Programming algorithm for UKP with Threshold Dominance and Periodicity
  
 In addition, you can use the CLI.exe console application to generate reports about the experiments listed below
 ### Available experiments
 1. Excel Report with comparison Genetic Algorithm combinations in order to select the best  (cp)
 2. Excel Report with measuring the performance and quality check of algorithms above  (mp)
 
 #### Example of use
```cmd
CLI.exe --report=mp --data_size=100 --task=ukp --runs=20 --instances=200
```
For more information, use:
```cmd
CLI.exe --help
```
