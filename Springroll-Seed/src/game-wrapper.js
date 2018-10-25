import {Application} from 'springroll';

export class GameWrapper
{
    constructor()
    {
        this.app = new Application({

        });

        this.game = UnityLoader.instantiate("gameContainer", "Build/WebGL.json", {onProgress: UnityProgress});
    }

    Unity_OnBeforeSceneLoad()
    {
        console.log('This is working');
    }

    Unity_OnAfterSceneLoad()
    {
        console.log('This is working too');
       
        setTimeout(() => {
            this.app.state.soundVolume.value = 0.5;
        }, 5000);
    }
}