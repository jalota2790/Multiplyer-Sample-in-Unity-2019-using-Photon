# Multiplyer-Sample-in-Unity-2019-using-Photon
This is an open PHOTON UNITY networking multiplayer demo and we are showing here how to make multiplayer game with random matchmaking where two players play in the same environment, shoot each other and the last one standing is the winner.

Scenes:
# Menu Scene: This scene handle matchmaking process, user is asked to enter the name then it connects to photon server and tries to find players, once maximum players enter the room, it navigates to game scene.

# Following are the part of this scene :
● Enter name panel: This panel contain input field and enter button.

● Room Panel: In this panel we have a layoutgroup and that contains list of current room.

● Datamanager: this is a singleton that is used to hold player name, we can easily use playerprefs to do this but using datamanager we can easily add new data feature that is why we use this.

● Photon game Object: in this game object we have added a photon view component and custom matchmaking scripts that handle connection and matchmaking.

# Game Scene: In this scene we have will have player in the same environment and players can fight each other .

# Following are the part of this scene:
● Environment: We have in environment a ground and player and also four spawn points .

● Health Slider: This is a UI slider that show current of local player.

● Win Panel: This panel is shown when local player wins the game.

● Lost Panel: This panel uses for when local player loses the game.

● Game Controller: game controller handles UI management , player generation and multiplayer events using RPC’s .

# Prefabs:
● Player: On player prefab we using Photon transform view and photon view and custom player controller. Visually it's a capsule model with a following camera and a gun that keeps position of a butte generation point.

● Bullet: bullet is a cube object that has two properties damage and speed. It has component bullet controller that moves it and handle collision with object and player.
