/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.filebrowserBrowseUrl = '/Editor/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Editor/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Editor/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Editor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Editor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/Editor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';


};