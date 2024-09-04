import * as monaco from 'monaco-editor';
import editorWorker from 'monaco-editor/esm/vs/editor/editor.worker?worker';
import tsWorker from 'monaco-editor/esm/vs/language/typescript/ts.worker?worker';
import { get } from './utils/fetch';

// @ts-ignore
self.MonacoEnvironment = {
	getWorker(_: any, label: string) {
		if (label === 'typescript' || label === 'javascript') {
			return new tsWorker();
		}
		return new editorWorker();
	}
};

monaco.languages.typescript.javascriptDefaults.setCompilerOptions({
	target: monaco.languages.typescript.ScriptTarget.ES2015,
	allowNonTsExtensions: true
});

// @ts-ignore
const result = await get('/api/v1/monaco');

// 便利result字典
for (const key in result) {
	if (Object.prototype.hasOwnProperty.call(result, key)) {
		const element = result[key];
		monaco.languages.typescript.javascriptDefaults.addExtraLib(element, key);
	}
}

monaco.languages.typescript.typescriptDefaults.setEagerModelSync(true);