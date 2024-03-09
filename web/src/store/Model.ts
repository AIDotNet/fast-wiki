import { Models } from "../models";

let models: Models;

async function loadingModel() {
    const response = await fetch('/model.json');
    models = await response.json();

}

export async function getModels() {
    if (!models) {
        await loadingModel();
    }
    return models;
}