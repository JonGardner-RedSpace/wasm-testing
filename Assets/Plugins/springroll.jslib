var springrollLib = {
    AddStateEvent: function(name, ptr)
    {
        console.log(Pointer_stringify(name), ptr);
        //Runtime.dynCall('vff', ptr, [0, 1]);
    },

    Unity_OnBeforeSceneLoad: function()
    {
        window.gameWrapper.Unity_OnBeforeSceneLoad();
    },

    Unity_OnAfterSceneLoad: function() {
        window.gameWrapper.Unity_OnAfterSceneLoad();
    }
}
mergeInto(LibraryManager.library, springrollLib);