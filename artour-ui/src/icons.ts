import { library } from '@fortawesome/fontawesome-svg-core';

import { faFacebookF } from '@fortawesome/free-brands-svg-icons/faFacebookF';
import { faTwitter } from '@fortawesome/free-brands-svg-icons/faTwitter';
import { faLinkedinIn } from '@fortawesome/free-brands-svg-icons/faLinkedinIn';
import { faRedditAlien } from '@fortawesome/free-brands-svg-icons/faRedditAlien';
import { faGooglePlusG } from '@fortawesome/free-brands-svg-icons/faGooglePlusG';
import { faVk } from '@fortawesome/free-brands-svg-icons/faVk';
import { faTelegramPlane } from '@fortawesome/free-brands-svg-icons/faTelegramPlane';


const icons = [
  faFacebookF, faTwitter, faLinkedinIn, faRedditAlien, faGooglePlusG, faVk, faTelegramPlane
];

library.add(...icons);