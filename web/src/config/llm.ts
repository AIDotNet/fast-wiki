export const getLLMConfig = () => {
  // region format: iad1,sfo1
  let regions: string[] = [];
  if (process.env.OPENAI_FUNCTION_REGIONS) {
    regions = process.env.OPENAI_FUNCTION_REGIONS.split(',');
  }

  return  {
    API_KEY_SELECT_MODE: process.env.API_KEY_SELECT_MODE,

    ENABLED_OPENAI: process.env.ENABLED_OPENAI !== '0',
    OPENAI_API_KEY: process.env.OPENAI_API_KEY,
    OPENAI_PROXY_URL: process.env.OPENAI_PROXY_URL,
    OPENAI_MODEL_LIST: process.env.OPENAI_MODEL_LIST,
    OPENAI_FUNCTION_REGIONS: regions as any,

    ENABLED_AZURE_OPENAI: !!process.env.AZURE_API_KEY,
    AZURE_API_KEY: process.env.AZURE_API_KEY,
    AZURE_API_VERSION: process.env.AZURE_API_VERSION,
    AZURE_ENDPOINT: process.env.AZURE_ENDPOINT,
    AZURE_MODEL_LIST: process.env.AZURE_MODEL_LIST,

    ENABLED_ZHIPU: !!process.env.ZHIPU_API_KEY,
    ZHIPU_API_KEY: process.env.ZHIPU_API_KEY,

    ENABLED_DEEPSEEK: !!process.env.DEEPSEEK_API_KEY,
    DEEPSEEK_API_KEY: process.env.DEEPSEEK_API_KEY,

    ENABLED_GOOGLE: !!process.env.GOOGLE_API_KEY,
    GOOGLE_API_KEY: process.env.GOOGLE_API_KEY,
    GOOGLE_PROXY_URL: process.env.GOOGLE_PROXY_URL,

    ENABLED_PERPLEXITY: !!process.env.PERPLEXITY_API_KEY,
    PERPLEXITY_API_KEY: process.env.PERPLEXITY_API_KEY,
    PERPLEXITY_PROXY_URL: process.env.PERPLEXITY_PROXY_URL,

    ENABLED_ANTHROPIC: !!process.env.ANTHROPIC_API_KEY,
    ANTHROPIC_API_KEY: process.env.ANTHROPIC_API_KEY,
    ANTHROPIC_PROXY_URL: process.env.ANTHROPIC_PROXY_URL,

    ENABLED_MINIMAX: !!process.env.MINIMAX_API_KEY,
    MINIMAX_API_KEY: process.env.MINIMAX_API_KEY,

    ENABLED_MISTRAL: !!process.env.MISTRAL_API_KEY,
    MISTRAL_API_KEY: process.env.MISTRAL_API_KEY,

    ENABLED_OPENROUTER: !!process.env.OPENROUTER_API_KEY,
    OPENROUTER_API_KEY: process.env.OPENROUTER_API_KEY,
    OPENROUTER_MODEL_LIST: process.env.OPENROUTER_MODEL_LIST,

    ENABLED_TOGETHERAI: !!process.env.TOGETHERAI_API_KEY,
    TOGETHERAI_API_KEY: process.env.TOGETHERAI_API_KEY,
    TOGETHERAI_MODEL_LIST: process.env.TOGETHERAI_MODEL_LIST,

    ENABLED_MOONSHOT: !!process.env.MOONSHOT_API_KEY,
    MOONSHOT_API_KEY: process.env.MOONSHOT_API_KEY,
    MOONSHOT_PROXY_URL: process.env.MOONSHOT_PROXY_URL,

    ENABLED_GROQ: !!process.env.GROQ_API_KEY,
    GROQ_API_KEY: process.env.GROQ_API_KEY,
    GROQ_PROXY_URL: process.env.GROQ_PROXY_URL,

    ENABLED_ZEROONE: !!process.env.ZEROONE_API_KEY,
    ZEROONE_API_KEY: process.env.ZEROONE_API_KEY,

    ENABLED_AWS_BEDROCK: process.env.ENABLED_AWS_BEDROCK === '1',
    AWS_REGION: process.env.AWS_REGION,
    AWS_ACCESS_KEY_ID: process.env.AWS_ACCESS_KEY_ID,
    AWS_SECRET_ACCESS_KEY: process.env.AWS_SECRET_ACCESS_KEY,

    ENABLED_OLLAMA: process.env.ENABLED_OLLAMA !== '0',
    OLLAMA_PROXY_URL: process.env.OLLAMA_PROXY_URL || '',
    OLLAMA_MODEL_LIST: process.env.OLLAMA_MODEL_LIST,

    ENABLED_QWEN: !!process.env.QWEN_API_KEY,
    QWEN_API_KEY: process.env.QWEN_API_KEY,

    ENABLED_STEPFUN: !!process.env.STEPFUN_API_KEY,
    STEPFUN_API_KEY: process.env.STEPFUN_API_KEY,
  };
};

export const llmEnv = getLLMConfig();
