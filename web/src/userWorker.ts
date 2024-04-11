import * as monaco from 'monaco-editor';
import editorWorker from 'monaco-editor/esm/vs/editor/editor.worker?worker';
import jsonWorker from 'monaco-editor/esm/vs/language/json/json.worker?worker';
import cssWorker from 'monaco-editor/esm/vs/language/css/css.worker?worker';
import htmlWorker from 'monaco-editor/esm/vs/language/html/html.worker?worker';
import tsWorker from 'monaco-editor/esm/vs/language/typescript/ts.worker?worker';
import { get } from './utils/fetch';

// @ts-ignore
self.MonacoEnvironment = {
	getWorker(_: any, label: string) {
		if (label === 'json') {
			return new jsonWorker();
		}
		if (label === 'css' || label === 'scss' || label === 'less') {
			return new cssWorker();
		}
		if (label === 'html' || label === 'handlebars' || label === 'razor') {
			return new htmlWorker();
		}
		if (label === 'typescript' || label === 'javascript') {
			return new tsWorker();
		}
		return new editorWorker();
	}
};

monaco.languages.register({ id: 'javascript' });

monaco.languages.setLanguageConfiguration('javascript', {
	indentationRules: {
		increaseIndentPattern: /^(.*\bcase\b\s*.*|.*\bdefault\b\s*.*)$/,
		decreaseIndentPattern: /^\s*[\}\]].*$/,
	},
	folding: {
		markers: {
			start: new RegExp('^\\s*//\\s*#?region\\b'),
			end: new RegExp('^\\s*//\\s*#?endregion\\b'),
		},
	},
	brackets: [
		['{', '}'],
		['[', ']'],
		['(', ')'],
	],
	wordPattern: /(-?\d*\.\d\w*)|([^`~!@#%^&*()=+[{\]}\\|;:'",.<>\/?\s]+)/g,
});
const formatCommand = {
	id: 'format-command',
	label: 'Format',
	keybindings: [
		monaco.KeyMod.chord(monaco.KeyMod.CtrlCmd | monaco.KeyCode.KeyK, monaco.KeyCode.KeyF) // 绑定到 Ctrl + K, Ctrl + F
	],
	run: function (editor: any) {
		// 格式化代码逻辑
		const model = editor.getModel();
		const range = model.getFullModelRange();
		editor.executeEdits('format-command', [{
			range: range,
			text: '格式化代码'
		}]);
	}
};

monaco.languages.typescript.javascriptDefaults.setCompilerOptions({
	target: monaco.languages.typescript.ScriptTarget.ES2015,
	allowNonTsExtensions: true
});

const result = await get('/api/v1/monaco');

// 便利result字典
for (const key in result) {
	if (Object.prototype.hasOwnProperty.call(result, key)) {
		const element = result[key];
		monaco.languages.typescript.javascriptDefaults.addExtraLib(element, key);
	}
}

monaco.languages.typescript.typescriptDefaults.setEagerModelSync(true);