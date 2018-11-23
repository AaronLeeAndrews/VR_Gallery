mergeInto(LibraryManager.library, {
  Hello: function () {
    window.alert("Hello, world!");
  },

  HelloString: function (str) {
    window.alert(Pointer_stringify(str));
  },

  PrintFloatArray: function (array, size) {
    for(var i = 0; i < size; i++)
    console.log(HEAPF32[(array >> 2) + i]);
  },

  AddNumbers: function (x, y) {
    return x + y;
  },

  StringReturnValueFunction: function () {
    var returnStr = "bla";
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  BindWebGLTexture: function (texture) {
    GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
  },
  

    CreateVideoDiv: function (str) {

        var div = document.createElement("iframe");
        div.id = "divVideo";
        div.style.width = "100%";
        div.style.height = "100%";

        div.style.top = "0%";
        div.style.right = "0%";

        div.src = Pointer_stringify(str);
        div.style.position = "absolute";

        var but = document.createElement("button");
        but.id = "divButton";
        but.style.width = "5%";
        but.style.height = "5%";
        but.style.position = "absolute";

        but.style.top = "0%";
        but.style.right = "0%";
        but.style.background = "red";
        but.innerHTML = "X";
        but.addEventListener("click",
			function(){
				var div = document.getElementById("divVideo");
				var but = document.getElementById("divButton");

				document.getElementsByClassName("webgl-content")[0].removeChild(div);
				document.getElementsByClassName("webgl-content")[0].removeChild(but);
			}
		);
        document.getElementsByClassName("webgl-content")[0].appendChild(div);
        document.getElementsByClassName("webgl-content")[0].appendChild(but);
  },
 

});