import { Langfuse } from 'langfuse';
import { CreateLangfuseTraceBody } from 'langfuse-core';

import { TraceEventClient } from '@/libs/traces/event';

/**
 * We use langfuse as the tracing system to trace the request and response
 */
export class TraceClient {
  private _client?: Langfuse;

  constructor() {
    return;
  }

  createEvent(traceId: string) {
    const trace = this.createTrace({ id: traceId });
    if (!trace) return;

    return new TraceEventClient(trace);
  }

  createTrace(param: CreateLangfuseTraceBody) {
    return this._client?.trace({ ...param });
  }

  async shutdownAsync() {
    await this._client?.shutdownAsync();
  }
}
