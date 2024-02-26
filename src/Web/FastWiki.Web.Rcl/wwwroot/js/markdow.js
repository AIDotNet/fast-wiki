window.MasaBlazor.extendMarkdownIt = function (parser) {

    const { md, scope } = parser;
    if (window.markdownitEmoji) {
        md.use(window.markdownitEmoji);
    }

    md.renderer.rules.code_block = renderCode(md.renderer.rules.code_block, md.options);
    md.renderer.rules.fence = renderCode(md.renderer.rules.fence, md.options);
}
function renderCode(origRule, options) {
    return (...args) => {
        const [tokens, idx] = args;
        const content = tokens[idx].content
            .replaceAll('"', '&quot;')
            .replaceAll("'", "&lt;");
        const origRendered = origRule(...args);

        if (content.length === 0)
            return origRendered;

        return `
<div style="position: relative;background-color:#d1d1d1;border-radius: 12px;overflow:auto;margin: 5px;">
	${origRendered}
	<button class="markdown-it-code-copy" onclick="copy('${encodeBase64(tokens[idx].content.trimEnd())}')" style="position: absolute; top: 5px; right: 5px; cursor: pointer; outline: none;" title="复制">
		<span style="font-size: 21px; opacity: 0.4;" class="mdi mdi-content-copy"></span>
	</button>
</div>
`;

    };
}

window.copy = async (e) => {
    copyText(decodeBase64(e))
    await DotNet.invokeMethodAsync('Gotrays.Rcl', 'Notifications', "复制成功")
}

function copyText(text) {
    /* 获取要复制的内容 */
    /* 创建一个临时的textarea元素 */
    var tempInput = document.createElement("textarea");
    tempInput.value = text;
    document.body.appendChild(tempInput);

    /* 选择并复制文本 */
    tempInput.select();
    document.execCommand("copy");

    /* 删除临时元素 */
    document.body.removeChild(tempInput);

}

const BASE64_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

function encodeBase64(input) {
    let output = "";
    let i = 0;
    const inputArray = new TextEncoder().encode(input);

    while (i < inputArray.length) {
        const byte1 = inputArray[i++] || 0;
        const byte2 = inputArray[i++] || 0;
        const byte3 = inputArray[i++] || 0;

        const b1 = byte1 >> 2;
        const b2 = ((byte1 & 0x03) << 4) | (byte2 >> 4);
        const b3 = ((byte2 & 0x0F) << 2) | (byte3 >> 6);
        const b4 = byte3 & 0x3F;

        if (!byte2) {
            output += BASE64_CHARS[b1] + BASE64_CHARS[b2] + '==';
            break;
        } else if (!byte3) {
            output += BASE64_CHARS[b1] + BASE64_CHARS[b2] + BASE64_CHARS[b3] + '=';
            break;
        } else {
            output += BASE64_CHARS[b1] + BASE64_CHARS[b2] + BASE64_CHARS[b3] + BASE64_CHARS[b4];
        }
    }

    return output;
}

function decodeBase64(input) {
    let output = "";
    let i = 0;
    let enc1, enc2, enc3, enc4;

    input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

    while (i < input.length) {
        enc1 = BASE64_CHARS.indexOf(input.charAt(i++));
        enc2 = BASE64_CHARS.indexOf(input.charAt(i++));
        enc3 = BASE64_CHARS.indexOf(input.charAt(i++));
        enc4 = BASE64_CHARS.indexOf(input.charAt(i++));

        const chr1 = (enc1 << 2) | (enc2 >> 4);
        const chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
        const chr3 = ((enc3 & 3) << 6) | enc4;

        output += String.fromCharCode(chr1);

        if (enc3 != 64) {
            output += String.fromCharCode(chr2);
        }
        if (enc4 != 64) {
            output += String.fromCharCode(chr3);
        }
    }

    return new TextDecoder().decode(new Uint8Array(output.split('').map(c => c.charCodeAt(0))));
}
