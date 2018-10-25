var springrollLib = {
    AddStateCallback__deps: ['wrapperFunc'],
    AddStateCallback: function(name, sig, ptr)
    {
        var state = { sig: Pointer_stringify(sig), ptr: ptr };
        window.gameWrapper.app.state[Pointer_stringify(name)].subscribe(_wrapperFunc.bind(state));
    },

    Unity_OnBeforeSceneLoad: function()
    {
        window.gameWrapper.Unity_OnBeforeSceneLoad();
    },

    Unity_OnAfterSceneLoad: function()
    {
        window.gameWrapper.Unity_OnAfterSceneLoad();
    },

    wrapperFunc: function(curr, last)
    {
        Runtime.dynCall(this.sig, this.ptr, [curr, last]);
    }
}
mergeInto(LibraryManager.library, springrollLib);