//
// Copyright (c) 2023 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { EventHandler } from '../types/Event';

import Presenter from './Header.presenter';

interface HeaderProps {
  onInputChange?: EventHandler<string>,
  onSubmit?: EventHandler
}

function Header(props: Readonly<HeaderProps>) {

  const {
    onInputChange,
    onSubmit
  } = props;

  return (
    <Presenter
      onInputChange={onInputChange}
      onSubmit={onSubmit} />
  );

}

export default Header;
