import React from 'react';
import serializeForm from 'form-serialize'
import SaveIcon from 'react-icons/lib/fa/floppy-o'


export default function AddControl({ department, saveDepartment }) {
    var handleSubmit = (e) => {
        e.preventDefault()
        const values = serializeForm(e.target, { hash: true })
        saveDepartment(values)
    }

    return (
        <div>
            <form onSubmit={handleSubmit} className="create-contact-form">
                    <div>
                        <h3>Add post</h3>
                        <input type="text" name="Name" className="text-editor" placeholder="Name" /><br />
                        <input type="text" name="WeightMin" className="editor-field" placeholder="Weight From" /><br />
                        <input type="text" name="WeightMax" className="editor-field" placeholder="Weight To" /><br />
                        <input type="text" name="PriceStart" className="editor-field" placeholder="Price" /><br />
                        
                    </div>
                <button className='icon-btn'>
                    <SaveIcon size={30} />
                </button>
            </form>
        </div>
    )
}