
import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import AccountStore from './stores/AccountStore';

const accountStore = new AccountStore();

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    <App accountStore={accountStore} />
  </React.StrictMode>
);