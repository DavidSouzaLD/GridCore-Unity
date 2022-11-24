# GridCore
[Description]

[3D/2D grid system for unity in C#, used through cells that hold any type of value.]

[Explanation]

-> How to use ?

-> Well, it's quite simple to use GridCore for your projects, be it procedural or predefined.

-> We start by creating a central script that is usually referred to by some name that serves as a handler.

-> This script, which we will use as an example as "World", will inherit GridCore<Type of object to be stored by the cell>.

-> And you can access the cells with the world values passed to the "World" using GetCell3d/GetCell2D, which as the names indicate, refer to the types of positions that will be applied to return the cells of that position.
