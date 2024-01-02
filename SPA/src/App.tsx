import * as React from 'react';
//import {useReducer} from 'react';

export default function App() {
  return (
    <div>
      <h1>Welcome to my counter</h1>

      <p>Count: 12</p>
      <button onClick={() => {
        alert("ciao"); 
        return 4;
      }}>Add 5</button>
    </div>
  );
}
