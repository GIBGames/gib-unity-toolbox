[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

# GIB Games Presents: Tools in a Bottle
## A collection of useful Unity scripts and extensions.


## Installation

Import the downloaded scripts into some location (preferably an incredibly inconvenient one) in your `Assets` folder.

## Usage

### Editor Windows

Several editor windows can be called from the *Window>Toast's Script Library* menu.

* **Replace Missing Mats** finds Missing Textures and replaces them all with the specified texture.
* **Inspector Lock** locks the Inspector with a keystroke.
* **Editor Physics Simulator** simulates physics in-editor, allowing for precise flush-to-floor placement of objects, as well as simulation of debris and other scattered things.
* **Find-A-Shader** finds Materials that match a string query and returns their names.

### Extension Methods
List and Transform extensions that help make Vector3 and transform data useful, such as:

* **List<T>.AddMulti()** allows you to add multiple elements to a list at once.
* **List.Shuffle()** shuffles a list. Ka-Pow!
* **GameObject.GetChildren()** returns a list of all children of the GameObject.
* **Vector3.ChangeX() / ChangeY() / ChangeZ()** Allows you to directly set transform axis positions.
* **Vector3.Flatten()** returns a Vector3 that ignores the Y axis.
* **Vector3.FlatDistance** returns a Vector3.Distance that ignores the Y axis.
* **Transform.LookAtRotation(Vector3/Transform/gameObject)** returns a quaternion necessary to look at a Vector3, Transform, or GameObject.

### Components
Some nifty components for debugging or, you know, whatever.

* **Ignore Collide** sets an object to ignore specific colliders, regardless of their settings in the Physics options.
* **DeveloperComments** is literally just a TextArea for notes.
* **UIWorldSpaceOverlay** Renders UI elements over the world geometry. Useful if subtitles or UI elements exist in world space, and you want them to always be visible.


## Contributing
Have you created a cool, useful Unity Editor script and want to add it to this library? Awesome! Pull requests are welcome. 

## License
[MIT](https://choosealicense.com/licenses/mit/)
