# iGraph C Sharp Mods

##### Note: This is currently building and running on mac osx. 

### Project Description 

This is adapted from Leo Ferres [iGraph Lite](https://github.com/leoferres/igraph)
This is a stripped back version that takes a JSON file as input and generates a HTML file.

Note:
- The JSON must describe a graph object in the same way as the example. This is currently a WIP as
	many methods for reading the JSON values are stubbed. *Will need to come back to this*.  
 - The generated HTML file is a natural language description of a the given graph data. Amazing huh? 

### Steps to download: 

1. Clone the repo `git clone <repo.git` 
2. Open up `iGraphLite.sln` (the solution) in Visual Studio
3. Look for any packages that you dont have on the system (you'll get warnings if they're missing)  and install them via nuget. 
4. When running the built solution (.exe) you will need to provide the following arguments:

Arguments:
> ```iGraph.exe -g -l 6 -f <full_path_to_json_file> -o <html_output_directory>```
    
Run in directory (ViSt only):   
>`<path_to_exe>` (I think...)

5. Put any test .JSON files in the /json folder, iGraphCLI documents are currently outputted here. 
