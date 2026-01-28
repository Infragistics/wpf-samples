# This is a comment. 
graph [ # Lists start with '['. 
directed 1 # This is a directed graph (0 for undirected). 

# The following is an object of type string. 
# It will be ignored unless you specify a rule for graph.text. 
text "This is a string object." 

node [ id 1 label "node 1" ] # This defines a node with id 1. 
node [ id 2 label "node 2"] 
node [ id 3 label "node 3"] 

edge [ # This defines an edge leading from node 1 to node 2. 
source 1 
target 2 
] 
edge [ 
source 2 
target 3 
] 
edge [ 
source 3 
target 1 
] 
] # Lists end with ']'. 
