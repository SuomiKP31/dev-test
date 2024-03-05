# Description
This project is designed to test the candidates ability to make a game with All Out. We are looking for developers that can learn quickly with little assistance, and can think about what could make playing the game enjoyable.
Your goal is to create a simple "simulator" style game. The general game loop should be: players earn money (eg. by collecting a resource), and then spend the money to make earning money easier (eg. upgrading a tool required to collect a resource).

# Requirements
 - Completed "simulator" gameplay loop
   - Example simulator loop you can use (or create your own if you're feeling creative):
     - Interact with a resource, and mash E to decrease it's health. Once the resource has zero health then the player collects it.
     - The player then sells these resources to a shop
     - The player can then spend the money they've earned on upgrades. Upgrades could be "power", and "multiplier. Power would decrease the amount of E presses it takes to collect resources, and multiplier would increase the amount resources sell for.
 - Use the in-game leaderboard to show some kind of score for the players (in addition to any UI you choose to show)
   - https://docs.allout.game/api/AO.Leaderboard
 - Use RPCs and/or SyncVars to sync things across the game
   - https://docs.allout.game/api/AO.SyncVar-1.html
   - https://docs.allout.game/api/AO.ClientRpc.html
   - https://docs.allout.game/api/AO.ServerRpc.html

# Nice to haves
 - Try using our code-based UI system
 - Bring in and use some assets.

# Notes
 - Placeholder art is totally acceptable
 - The existing code is just there to be something to build on. Feel free to remove, or modify any of it.
 - All Out is still very new, if you run into issues or crashes please reach out and we can work to get it resolved.
 - Be sure to check out the docs! https://docs.allout.game/docs/getting-started.html

# Instructions
 1. Clone this repository
 2. Go to https://allout.game
 3. Login with the credentials we give you.
 4. Go to Create > Click on the project
 5. Open the ao.project file and replace the project id
    - You can find the project id in the URL on the website
 6. From the project page click "Launch Editor"
 5. Make your changes to the project.
 6. Publish your game with the in-game publish button
 7. Create a new git repository and share it with us, commit and push your changes there.