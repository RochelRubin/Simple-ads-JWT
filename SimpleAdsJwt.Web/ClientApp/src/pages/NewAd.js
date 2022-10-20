import React, { useState } from 'react';
import getAxios from '../AuthAxios';
import { useHistory } from 'react-router-dom';

const NewAd= () => {
    const history = useHistory();
    const [formData, setFormData] = useState({
        phoneNumber: '',
        text: '',
        
    });
    const onTextChange = e => {
        const copy = { ...formData };
        copy[e.target.name] = e.target.value;
        setFormData(copy);
    }

    const onFormSubmit = async e => {
        e.preventDefault();
        await getAxios().post('/api/ad/addad', formData);
        history.push('/');
        
    }
    return (
       <>
        <div className="row">
            <div className="col-md-6 offset-md-3">
                <h2>New Ad</h2>
            </div>
        </div>

        <div className="row">
            <div className="col-md-6 offset-md-3 jumbotron">
                <form onSubmit={onFormSubmit}>
                    <div className="row">
                        <div className="col-md-12">
                            <input value={formData.phoneNumber} type="text" onChange={onTextChange} placeholder="Phone Number" className="form-control" name="phoneNumber" />
                        </div>
                    </div>
                    <br />
                    <textarea value={formData.text} name="text"onChange={onTextChange} className="form-control" rows="10" placeholder="Description"></textarea>
                    <button className="btn btn-primary">Submit</button>
                </form>
            </div>
        </div>
       </>
    );
}
export default NewAd;