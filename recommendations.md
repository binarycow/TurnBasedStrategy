
## General notes

#### Code Style

Anything marked as [Code Style] is a stylistic choice.  You should pick a style 
and stick with it.  My recommendations here are what **I** do - you do not need
to pick this style.  No matter what style you pick, you should be consistent 
throughout the code base.  Consider setting up your IDE to warn/enforce your style
settings.  Consider using [EditorConfig](https://editorconfig.org/)

#### Monogame

Monogame specific advice is tagged [Monogame].

#### General recommendations

If I didn't tag it as something else (above), then it's just general recommendations
that should apply in all situations.

Take a look at [Monogame.Extended](https://github.com/craftworkgames/MonoGame.Extended)
- [Screen Management](https://www.monogameextended.net/docs/features/screen-management/screen-management)

## Everywhere
1. Use explicit accessibility modifiers [Code Style]
2. Remove unused `using` namespace directives
3. Sort `using` namespace directives
    1. Typical groupings are usually, `System.*` first, then libraries, then your code
    2. Typically within each group, alphabetical sort
4. Use a `this.` prefix before member fields, properties, and methods. [Code Style]
5. [Avoid braceless](https://softwareengineering.stackexchange.com/questions/16528/single-statement-if-block-braces-or-no) 
   `if`, `while`, `for` statements [Code Style]
6. Consider enabling [nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references)
7. If member fields are only initialized in the constructor, make them `readonly`
8. Prefer to use patterns that minimize the number of things that *could* be null.
9. Prefer pattern matching over comparing equality with null
    1. Prefer `item is null` over `item == null`
    2. Prefer `item is not null` over `item != null` (if using C# 9)
    3. Prefer `!(item is null)` over `item != null` (if using C# 8 or below)
10. 

## File/type specific recommendations

#### Program

1. Use `using` declaration syntax [Code Style]

#### Interfaces

1. Type name should match filename; rename `Interfaces.cs` to `IScreen.cs`
2. Namespace does not match file location; change namespace to `StrategyGame`

#### StrategyGame

1. `graphics` can be made readonly

#### ScreenManager

If you make `ScreenManager` implement `DrawableGameComponent`, you no longer need to manage `Draw` and `Update` in `StrategyGame`...
in fact, `StrategyGame` merely sets up all the components; the rest of it runs the show.

Note, that there is some debate whether or not the `Component` system is the right call.  Do your own research
and make your decision

- http://cowboyprogramming.com/2007/01/05/evolve-your-heirachy/
- https://gamedev.stackexchange.com/questions/9204/what-are-the-cons-of-using-drawablegamecomponent-for-every-instance-of-a-game-ob/9209#9209
- https://gavsdevblog.wordpress.com/2016/09/04/monogame-components-and-services/
- https://community.monogame.net/t/gamecomponent-vs-custom-class/7606

Other thoughts:

`ScreenManager` does a lot of things that don't seem to be screen related.  `ToggleMusic` for instance.  It seems to me that
it would be better to have an object that manages settings and stuff, so when the user toggles the background music in `MenuScreen`,
a property on the settings object is changed.

You could have a class `AudioManager`, which implements `GameComponent`.  When `Update` is called, it would check the settings object,
to see if audio is enabled or disabled.  If disabled, it would stop the audio.

#### Settings

1. Are your tile sizes going to change?  I'm assuming that these are fairly set in place.  Maybe use a `const` for these?


Other thoughts:

Consider having other types of `Settings` objects.  Your color settings could be in `GraphicalSettings`, for example.
You could use these `Settings` objects to keep track of the user's resolution/audio preferences; these could be serialized
so the settings are picked up when the game opens.

#### Grid

I'm not sure storing the number of rows/columns in `Settings` is the right call here.  What if you need a different `Grid`
with a different number of rows/columns?  Makes more sense to me that these are properties on the `Grid`,
or passed via constructor.

#### GameObject

In my mind, `GameObject` refers to things like players, units, weapons, inventory items, etc.  These objects should not
be responsible for drawing or updating themselves - like `GameObject.Draw` and `GameUpdate.Update` requires.

I'm thinking that there should be an object which represents the game itself (not necessarily the *graphical* representation
of a game, but the game itself - this is how it is different than `StrategyGame`).  You would call `Update` on this object,
which would update all of the game objects - players, units, weapons, etc.  Then, a **seperate** object (maybe 
`GameRenderer`) will actually render the state of the game.