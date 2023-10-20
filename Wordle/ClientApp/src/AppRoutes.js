import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import Game from './components/Game';
import Profile from './components/Profile';
import {Home} from "./components/Home";
import NewHome from './components/NewHome';
import Error from './components/Error';

const AppRoutes = [
  {
    index: true,
    element: <NewHome />
  },
  {
    path: "/game",
    element: <Game/>
  },
  {
    path: "/profile",
    element: <Profile/>
  },


  ...ApiAuthorzationRoutes,
  {
    path: "*",
    element: <Error/>
  },
];

export default AppRoutes;
