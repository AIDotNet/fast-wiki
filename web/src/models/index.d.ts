export interface ChatApplicationDto {
    id: string;
    name: string;
    prompt: string;
    chatModel: string;
    temperature: number;
    maxResponseToken: number;
    template: string;
    parameter: { [key: string]: string; };
    opener: string;
    wikiIds: number[];
    referenceUpperLimit: number;
    noReplyFoundTemplate: string | null;
    showSourceFile: boolean;
    relevancy: number;
}

export interface ChatDialogDto {
    id: string;
    name: string;
    chatId: string;
    description: string;
    type: ChatDialogType;
    creationTime: string;
    typeName: string;
    isEdit: boolean;
}

export interface ChatDialogHistoryDto {
    id: string;
    chatDialogId: string;
    content: string;
    tokenConsumption: number;
    current: boolean;
    type: ChatDialogHistoryType;
    sourceFile: SourceFileDto[];
    creationTime: string;
}

export interface ChatShareCompletionsInput {
    guestId: string;
    chatDialogId: string;
    chatShareId: string;
    content: string;
}


export interface ChatShareDto {
    id: string;
    name: string;
    chatApplicationId: string;
    expires: string;
    availableToken: number;
    availableQuantity: number;
}
export interface CompletionsDto {
    content: string;
    sourceFile: SourceFileDto[];
}

export interface SourceFileDto {
    name: string;
    filePath: string;
    fileId: string;
}

export interface CompletionsInput {
    chatDialogId: string;
    chatId: string;
    content: string;
}

export interface CreateChatApplicationInput {
    name: string;
}


export interface CreateChatDialogHistoryInput {
    chatDialogId: string;
    content: string;
    current: boolean;
    type: ChatDialogHistoryType;
}

export interface CreateChatDialogInput {
    name: string;
    chatId: string;
    description: string;
    applicationId: string;
    type: ChatDialogType;
}


export interface CreateChatShareInput {
    name: string;
    chatApplicationId: string;
    expires: string;
    availableToken: number;
    availableQuantity: number;
}

export interface SourceFileItem {
    fileName: string;
    content: string;
}

export interface UpdateChatApplicationInput {
    id: string;
    name: string;
    prompt: string;
    chatModel: string;
    temperature: number;
    maxResponseToken: number;
    template: string;
    parameter: { [key: string]: string; };
    opener: string;
    noReplyFoundTemplate: string | null;
    showSourceFile: boolean;
    wikiIds: number[];
}

export enum ChatDialogType {
    ChatApplication,
    ChatShare
}

export interface CreateWikiDetailDataInput {
    wikiId: number;
    name: string;
    fileId: number;
    filePath: string;
    state: string;
    subsection: number;
    mode: ProcessMode;
    trainingPattern: TrainingPattern;
}

export interface CreateWikiDetailsInput {
    wikiId: number;
    name: string;
    fileId: number;
    filePath: string;
    subsection: number;
    mode: ProcessMode;
    trainingPattern: TrainingPattern;
}


export interface CreateWikiDetailWebPageInput {
    wikiId: number;
    name: string;
    path: string;
    state: string;
    subsection: number;
    mode: ProcessMode;
    trainingPattern: TrainingPattern;
}

export interface PaginatedListBase<TEntity> {
    total: number;
    result: TEntity[];
}

export interface WikiDto {
    id: number;
    icon: string;
    name: string;
    model: string;
    embeddingModel: string;
}

interface ChatModel {
    label: string;
    value: string;
}

interface EmbeddingModel {
    label: string;
    value: string;
}

interface Models {
    chatModel: ChatModel[];
    embeddingModel: EmbeddingModel[];
}


export enum TrainingPattern {
    Subsection,
    QA
}

export enum ProcessMode {
    Auto,
    Custom
}
export interface WikiDetailVectorQuantityDto {
    id: string;
    content: string;
    document_Id: string;
    wikiDetailId: string;
    fileId: string;
    index: number;
}