# Car AI using Genetic Algorithms in Unity

Unity project in which cars use Genetic Algorithms to improve driving in a given track.

Each car has one codification (an array of integers) that defines its behaviour when driving. Each of these integers defines one parameter such as:

* How much does it brake
* For how long does it brake
* The maximum obstacle detection distance
* Angle between the two rays for obstacle detection
* Etc

Each unit is tested and given a score (heuristic) based on its covered distance and time. After each generation of N units, the best ones are selected and crossed between them (for example, the first half of the codification of unit A with the second half of unit B). Because the units whose heuristic was higher are selected each time, each generation improves its driving in the track that's being tested.

## TODO:

* Make sure initial random units are not the same!
* Create a different track to test
* Display unit codification in the screen
* Allow multiple car "tests" at the same time (instead of having to see all of them one after the other)

## Tools

* ```Unity```

## Authors

* [David Moreno](https://github.com/mbdavid2)