using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CIS580.Input;

public interface IInputState
{
    bool IsKeyPressed(Keys key);

    bool IsNewKeyPress(Keys key);

    bool IsButtonPressed(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex);

    bool IsNewButtonPress(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex);
}
