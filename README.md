# Force-directed Diagram Viewer

This application, written in C#, lets you visually compare the output from both algorithms as well as their generation statistics. Three sample data sets are included.

![Application screenshot](https://github.com/mderenardi/force-directed-diagram-viewer/raw/master/screenshot.jpg)

Based upon [Bradley Smith's blog post](http://www.brad-smith.info/blog/archives/129) on force-directed diagrams, this algorithm uses a faster node-pair algorithm. Instead of calculating forces for every node, it only calculates for unique node pairs which reduces the total number of calculations and allows reuse of redundant values. This algorithm generally runs approximately +2x faster than the standard algorithm but due to reduced iterations, suboptimal dispersal of nodes occurs more often.

Released under the BSD license.
