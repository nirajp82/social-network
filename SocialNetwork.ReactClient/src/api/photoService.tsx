import httpService from './httpService';
import { IPhoto } from '../models/IProfile';

const photoService = {
    upload: (file: Blob): Promise<IPhoto> => {
        let formData = new FormData();
        formData.append('File', file);
        return httpService.postForm('photo/', formData);
    },
    setMain: (photoId: string): Promise<void> => {
        return httpService.post(`photo/${photoId}/setmain`, photoId);
    },
    delete: (photoId: string): Promise<void> => {
        return httpService.delete(`photo/${photoId}`);
    }
};

export default photoService;