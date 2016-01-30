# Algorithms

Folder structure + Algorithms

Algorithms  
-Java  
--Src // where all our .Java files are  
----NoisyNeighbours // Counting, bitmasks    
---670  
----Cdgame // counting  
----DrBalance  // strings, minimization  
----Treestrat  // graph  
---671  
----BearPaints // Maximization  
----BearDarts2  // counting  
-Cpp  
--Header // where all our .h files are  
--Source // where all our .cpp files are  
---2012R1B.cpp  
----SafetyInNumbers (Binary Search: Continous)   
  

<h2>How to setup on local machine after pull</h2>  
  
<h3>Java Setup (Eclipse)</h3>  
Create a new project named Java  
Location: ~git/Agorithms/Java  
Import the src folder into "src" in eclipse  
Right click project Run As > Run Configuration > Java Application. Select the main class  
  
<h3>Cpp Setup (Visual studio)</h3>  
Create new empty project named Cpp  
Location: ~git/Algorithms/Cpp     
Right click on project and add existing item  
Add select the cpp files
  
<h3>Csharp setup</h3>  
Important: see code comment on how to add resources
  
  
<h3>Others</h3>
Possible TODOs  
Synchornize cpp local file sys  

Others  
Java Issues  
How to open project: use peter's solution and adding resources forward slash in front of resource file  
http://stackoverflow.com/questions/2636201/how-to-create-a-project-from-existing-source-in-eclipse-and-then-find-it  

How to get the reousrces loaded  correctly
http://stackoverflow.com/questions/573679/open-resource-with-relative-path-in-java  
  
CPP Setup  
for some reason, Visual studio when creating a project by VS called proj1, the source files are placed in proj1/proj1. So for example if your source file is Main.cpp, it will be in proj1/proj1/Main.cpp. In order to set up VS with all the correct files,   
1. Name your project Cpp  
2. Create from C:/git/Algorithms. Your source files will be here C:/git/Algorithms/Cpp/Cpp/*.cpp  
3. Then add all your source files in. You can skip the project or sln items  
  
  
