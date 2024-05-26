import StoreUpdater, { StoreUpdaterProps } from './StoreUpdater';
import { Provider, createStore } from './store';

type AgentSettingsProps = StoreUpdaterProps;

const AgentSettings = (props: AgentSettingsProps) => {
  return (
    <Provider createStore={createStore}>
      <StoreUpdater {...props} />
    </Provider>
  );
};

export default AgentSettings;
