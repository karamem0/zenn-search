//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import ReactDOM from 'react-dom/client';

import { webLightTheme } from '@fluentui/react-components';

import { Global } from '@emotion/react';
import * as ress from 'ress';

import MainPage from './pages/MainPage';
import TelemetryProvider from './providers/TelemetryProvider';
import ThemeProvider from './providers/ThemeProvider';

ReactDOM
  .createRoot(document.getElementById('root') as Element)
  .render(
    <React.StrictMode>
      <Global styles={ress} />
      <TelemetryProvider>
        <ThemeProvider theme={webLightTheme}>
          <MainPage />
        </ThemeProvider>
      </TelemetryProvider>
    </React.StrictMode>
  );
