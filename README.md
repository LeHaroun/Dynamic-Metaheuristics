# Dynamic-Metaheuristics
 An experiment on self regulating metaheuristics, 
 
 Generally Metaheuristic Algorithms dealing with intricate problems use combinations of mechanisms to represent, explore or improve on existing solutions. 
 
 Some of these mechanisms are Intelligent Exploration and Intelligent Intensification. 
 
 While searching for a good solution candidate and algorithm might stumble upon a region of the search space where the solutions are bad (e.g.. wrong lock combination, wrong encoding, overconsrained environment...) the algorithm should let go of the present region and EXPLORE other areas. 
 
 Again, if the odds are in favor of the search procedure, any given algorithm should not change the actual mechanisms that are generating good candidates, the algorithm should stay in the present context of solution creation and INTENSIFY the search in hope of getting the best solution. 
 
 The present code experiments with self regulating mechanisms, through dynamic parameter tuning, here we see an ANT COLONY OPTIMIZATION algorithm tuning its proper pheromone control parameters to achieve a desired mix of EXPLORATION and INTENSIFICATION depending on Qualitative evaluation of candidates in the previous search iteration.  
 
 
 
 ![Generic implementation](https://media.springernature.com/original/springer-static/image/chp%3A10.1007%2F978-3-319-91086-4_17/MediaObjects/105616_3_En_17_Fig2_HTML.png)
 
 
 
