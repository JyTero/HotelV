Hotel V is a hobby project of mine in which I’ll be slowly building a hotel management game with smart and individual characters. Player constructs and manages the hotel through a group of player-controlled characters. 

I’m currently building the basics for a utility AI which will serve as the foundation for most all characters within the game. The development happens within the Dev branch with main having anything semi-playable, which is currently nothing, so please do check the Dev branch. I tend to keep the build here functioning and error free, hence sparse pushes.

Current notable features include:  
- Characters are driven by Utility AI which chooses interaction based on character needs. (Trait weighting planned) 
- Characters have needs which they take care of themselves. 
- Characters can have traits which affect how they behave. Currently: 
  - Vampire trait: Removes Hunger and Energy needs, adds blood need. Unlocks ability to feed off of human characters. 
  - Talkative trait: Adds Social need. Makes Chat interaction take longer, gives more relationship points for chatting 
- Characters track relationship scores between each other. 
- UI to display needs, relationships and traits + some debug info such as current interaction and queued interaction 
- Objects, interactions and traits can be added with relative ease 
  - Interactions and traits are ScriptableObjects which all interested parties reference 

The inspirations for the project include The Sims 2/3, RimWorld and Vampire: the Masquerade. 

Due to being a hobby project, documentation is sporadic and not stored within the repo. Shoot a message / comment / something if you wish to learn more, would love to talk about the project in-depth. 

For any questions, write up into Discussions or send me a message on a platform of your choice, I’d love to talk about the project! 

Shared under CC BY-NC-ND 4.0
