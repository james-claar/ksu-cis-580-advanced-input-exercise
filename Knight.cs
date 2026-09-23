using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdvancedInputExercise;

/// <summary>
/// A sprite that represents a knight character in a platformer game.
/// </summary>
public class Knight 
{
    // The animation states for the knight sprite
    private enum KnightState { 
        Idle = 0, 
        Running = 1, 
        Jumping = 2, 
        Falling = 3 
    }

    // The knight animation state 
    private KnightState _currentState = KnightState.Idle;
    private Texture2D[] _animations = new Texture2D[4];
    private Rectangle _sourceRectangle = new Rectangle(0, 0, 120, 80);
    private int _frameWidth = 120;
    private float _frameTime = 0.0f;
    private float _frameDuration = 0.33f; // Duration of each frame in seconds
    private int _frameIndex  = 0;
    private bool _isFacingRight = true;

    // The knight's jumping state
    private bool _isJumping = false;
    private bool _isFalling = false;
    private float _jumpTimer = 0.0f;
    private float _maxJumpTime = 0.5f; // Maximum time the jump button can be held to reach max height


    // The knight's movement state
    private float _movementSpeed = 100.0f; // Speed of the knight's movement
    private Vector2 _position = new Vector2(100, 400);

    /// <summary>
    /// Load the animations for the knight sprite
    /// </summary>
    /// <param name="content">The content manager to use for loading assets</param>
    public void LoadContent(ContentManager content) 
    {
        _animations[(int)KnightState.Idle] = content.Load<Texture2D>("Knight/_Idle");
        _animations[(int)KnightState.Running] = content.Load<Texture2D>("Knight/_Run");
        _animations[(int)KnightState.Jumping] = content.Load<Texture2D>("Knight/_Jump");
        _animations[(int)KnightState.Falling] = content.Load<Texture2D>("Knight/_Fall");
    }

    /// <summary>
    /// Updates the sprite.
    /// </summary>
    /// <param name="gameTime">The current game time</param>
    public void Update(GameTime gameTime) 
    {
        // Default to idle state at the start of each update
        _currentState = KnightState.Idle; 

        // Trigger jump if pressed and not already jumping or falling
        if(false && !_isJumping && !_isFalling) // TODO: Check for jump input 
        {
            _isJumping = true;
            _jumpTimer = 0.0f; // Reset jump timer
            _frameTime = 0; // Move back to first frame
        }

        // Handle jumping logic 
        if(_isJumping)
        {
            _jumpTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_jumpTimer < _maxJumpTime) 
            {
                _position.Y -= _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; // Move up
                _currentState = KnightState.Jumping;
            } 
            else
            {
                _isJumping = false;
                _isFalling = true;
                _currentState = KnightState.Falling;
            }
        }

        // Handle falling logic 
        if(_isFalling)
        {
            _position.Y += _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; // Move down
            _currentState = KnightState.Falling;

            if(_position.Y >= 400) // Ground level
            {
                _position.Y = 400;
                _isFalling = false;
                _currentState = KnightState.Idle;
            }
        }

        // Handle left and right movement
        if(false) // TODO: Check for left movement 
        {
            if(_isFacingRight) 
            {
                _position.X -= 20; // Adjust position to account for sprite flipping
            }
            _isFacingRight = false;
            _position.X -= _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; // Move left
            if(!_isJumping && !_isFalling) 
            {
                _currentState = KnightState.Running;
            }
        } 
        else if(false) // TODO: Check for right movement 
        {
            if(!_isFacingRight) 
            {
                _position.X += 20; // Adjust position to account for sprite flipping
            }
            _isFacingRight = true;
            _position.X += _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; // Move right
            if(!_isJumping && !_isFalling) 
            {
                _currentState = KnightState.Running;
            }
        } 

        // Update animation
        _frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(_frameTime >= _frameDuration) 
        {
            _frameTime = 0.0f;
            _frameIndex++;
            // Check for the end of the animation and loop back to the first frame if necessary
            if (_frameIndex >= _animations[(int)_currentState].Width / _frameWidth) 
            {
                _frameIndex = 0;
            }
            _sourceRectangle.X = _frameIndex * _frameWidth;
        }

    }

    /// <summary>
    /// Renders the sprite to the screen.
    /// </summary>
    /// <param name="gameTime">The current game time</param>
    /// <param name="spriteBatch">The sprite batch to use for rendering</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
    {
        SpriteEffects spriteEffects = _isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        spriteBatch.Draw(_animations[(int)_currentState], _position, _sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, spriteEffects, 0f);
    }

}